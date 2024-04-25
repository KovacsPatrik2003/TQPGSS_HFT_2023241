let drivers = [];
let connection = null;
let driverIdToUpdate = -1;
let mostWinsResult = [];
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18928/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DriverCreated", (user, message) => {
        getdata();
    });

    connection.on("DriverDeleted", (user, message) => {
        getdata();
    });

    connection.on("DriverUpdated", (user, message) => {
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
    
    await fetch("http://localhost:18928/driver")
        .then(x => x.json())
        .then(y => {
            drivers = y;
            //console.log(drivers);
            display();
        });
}

function display() {
    document.getElementById('driverResultArea').innerHTML = "";
    drivers.forEach(t => {
        document.getElementById('driverResultArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.age + "</td><td>" + t.points + "</td><td>"
        + `<button type="button" onclick="remove(${t.id})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + " </td></tr>";
        //console.log(t.name);
    });
    
}

function create() {
    let dname = document.getElementById('driverName').value;
    let dpoints = document.getElementById('driverPoints').value;
    fetch('http://localhost:18928/driver', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: dname, points:dpoints })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function remove(id){
    fetch('http://localhost:18928/driver/' + id, {
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
    document.getElementById('driverNametoupdate').value = drivers.find(t => t['id'] == id)['name'];
    document.getElementById('driverPointstoupdate').value = drivers.find(t => t['id'] == id)['points'];
    document.getElementById('updateformdiv').style.display = 'flex';
    driverIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let dname = document.getElementById('driverNametoupdate').value;
    let dpoints = document.getElementById('driverPointstoupdate').value;
    fetch('http://localhost:18928/driver', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: dname, points: dpoints,id:driverIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

//noncrud resz **************************

function removeNonCrudResult() {
    document.getElementById('resultId').innerHTML = '';
}

function getResultMostWins() {
    document.getElementById('resultId').innerHTML = '';
    fetch('http://localhost:18928/DriverStat/WhoWonTheMost')
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
function getResultDriverWins() {
    document.getElementById('resultId').innerHTML = '';
    const alma = document.getElementById('driverName').value;
    console.log(alma);
    fetch('http://localhost:18928/DriverStat/DriverWins/Sergio%20Perez')
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


function getResultAvaragePoints() {
    document.getElementById('resultId').innerHTML = '';
    fetch('http://localhost:18928/DriverStat/AvaragePointPerGrandPrixByDrivers')
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



