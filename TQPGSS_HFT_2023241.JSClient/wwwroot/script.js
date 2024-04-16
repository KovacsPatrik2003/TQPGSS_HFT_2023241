let drivers = [];

getdata();

async function getdata() {
    await fetch("http://localhost:18928/driver")
        .then(x => x.json())
        .then(y => {
            drivers = y;
            console.log(drivers);
            display();
        });
}




function display() {
    drivers.forEach(t => {
        document.getElementById('driverResultArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.age + "</td><td>" + t.points + "</td></tr>";
        console.log(t.name);
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