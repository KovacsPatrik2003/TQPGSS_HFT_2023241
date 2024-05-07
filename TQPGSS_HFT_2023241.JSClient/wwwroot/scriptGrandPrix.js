let grandprixs = [];
let connection = null;
let grandprixIdToUpdate = -1;
getdata();
setupSignalR();
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18928/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GrandPrixCreated", (user, message) => {
        getdata();
    });

    connection.on("GrandPrixDeleted", (user, message) => {
        getdata();
    });

    connection.on("GrandPrixUpdated", (user, message) => {
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
    await fetch("http://localhost:18928/grandprix")
        .then(x => x.json())
        .then(y => {
            grandprixs = y;
            console.log(grandprixs);
            display();
        });
}

function display() {
    document.getElementById('grandPrixResultArea').innerHTML = "";
    grandprixs.forEach(t => {
        document.getElementById('grandPrixResultArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.date + "</td><td>" + t.whoWon + "</td><td>"
        + `<button type="button" onclick="remove(${t.id})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.id})">Update</button>`
        + `<button type="button" onclick="getResultWinner(${t.id})">Winner</button>`
        + `<button type="button" onclick="getResultDetails(${t.id})">Details</button>`
            + " </td></tr>";
        //console.log(t.name);
    });
}



function create() {
    let gname = document.getElementById('grandPrixName').value;
    let gdate = document.getElementById('Date').value;
    let gwinner = document.getElementById('Winner').value;
    fetch('http://localhost:18928/grandprix', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: gname, date: gdate, whoWon: gwinner })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}



function remove(id) {
    fetch('http://localhost:18928/grandprix/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(id) {
    document.getElementById('grandPrixNametoupdate').value = grandprixs.find(t => t['id'] == id)['name'];
    document.getElementById('Datetoupdate').value = grandprixs.find(t => t['id'] == id)['date'];
    document.getElementById('Winnertoupdate').value = grandprixs.find(t => t['id'] == id)['whoWon'];
    document.getElementById('updateformdiv').style.display = 'flex';
    grandprixIdToUpdate = id;
}



function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gname = document.getElementById('grandPrixNametoupdate').value;
    let gdate = document.getElementById('Datetoupdate').value;
    let gwinner = document.getElementById('Winnertoupdate').value;
    fetch('http://localhost:18928/grandprix', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: gname, date: gdate, whoWon: gwinner, id: grandprixIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

//noncrud resz ****************


function removeNonCrudResult() {
    document.getElementById('resultId').innerHTML = '';
}

function getResultWinner(id) {
    document.getElementById('resultId').innerHTML = '';
    const help_name = grandprixs.find(t => t['id'] == id)['name'];
    const newname = help_name.replace(" ", "%20")
    fetch('http://localhost:18928/GrandPrixStat/WinnerOfTheCircuit/'+newname)
        .then(response => response.text())
        .then(data => {
            const resultDiv = document.getElementById('resultId');
            resultDiv.innerHTML = `<p>${data}</p>`;
        })
        .catch(error => {
            console.error('Error:', error);
            const resultDiv = document.getElementById(resultId);
            resultDiv.innerHTML = '<p>An error occurred while fetching data</p>';
        });
}
function getResultDetails(id) {
    document.getElementById('resultId').innerHTML = '';
    const help_name = grandprixs.find(t => t['id'] == id)['name'];
    const newname = help_name.replace(" ", "%20")
    fetch('http://localhost:18928/GrandPrixStat/GrandPrixDetails/'+newname)
        .then(response => response.text())
        .then(data => {
            const resultDiv = document.getElementById('resultId');
            resultDiv.innerHTML = `<p>${data}</p>`;
        })
        .catch(error => {
            console.error('Error:', error);
            const resultDiv = document.getElementById(resultId);
            resultDiv.innerHTML = '<p>An error occurred while fetching data</p>';
        });
}