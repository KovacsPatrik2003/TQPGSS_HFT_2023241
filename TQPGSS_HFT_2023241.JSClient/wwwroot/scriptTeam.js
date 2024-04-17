let teams = [];
let connection = null;

let teamIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18928/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        getdata();
    });

    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });

    connection.on("TeamUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch("http://localhost:18928/team")
        .then(x => x.json())
        .then(y => {
            teams = y;
            console.log(teams);
            display();
        });
}

function display() {
    document.getElementById('teamResultArea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('teamResultArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.principal + "</td><td>" + t.points + "</td><td>"
        + `<button type="button" onclick="remove(${t.id})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + " </td></tr>";
        console.log(t.name);
    });
}



function create() {
    let gname = document.getElementById('teamName').value;
    let gpoints = document.getElementById('teamPoints').value;
    let gprincipal = document.getElementById('teamPrincipal').value;
    fetch('http://localhost:18928/team', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: gname, points: gpoints, principal:gprincipal})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}




function remove(id) {
    fetch('http://localhost:18928/team/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });;
}


function showupdate(id) {
    document.getElementById('teamNametoupdate').value = teams.find(t => t['id'] == id)['name'];
    document.getElementById('teamPointstoupdate').value = teams.find(t => t['id'] == id)['points'];
    document.getElementById('teamPrincipaltoupdate').value = teams.find(t => t['id'] == id)['principal'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teamIdToUpdate = id;
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gname = document.getElementById('teamNametoupdate').value;
    let gpoints = document.getElementById('teamPointstoupdate').value;
    let gprincipal = document.getElementById('teamPrincipaltoupdate').value;
    fetch('http://localhost:18928/team', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: gname, points: gpoints, principal: gprincipal, id: teamIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}