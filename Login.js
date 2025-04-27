document.getElementById("loginForm").addEventListener("submit", async function(event) {
    event.preventDefault();  

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const response = await fetch("http://localhost:5160/api/Auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
    });

    const data = await response.json();

    if (response.ok) {
        const token = data.token; 
        //stockage pour réutilisation
        localStorage.setItem("jwtToken", token);

        window.location.href = "/game";  //le nom de la scene
    } else {
        
        document.getElementById("errorMessage").innerText = data.message || "Login failed!";
        document.getElementById("errorMessage").style.display = "block";
    }
});


function logout() {
    localStorage.removeItem("jwtToken");  
    window.location.href = "/login";  
}

/*const token = localStorage.getItem("jwtToken"); // Récupérer le token stocké dans le localStorage

const response = await fetch("http://localhost:5160/api/Statistics/statistics", {
    method: "GET",
    headers: {
        "Authorization": `Bearer ${token}`, 
        "Content-Type": "application/json", 
    },
});

if (response.ok) {
    const data = await response.json();
    console.log("Statistics:", data); 
} else {
    alert("Unauthorized access"); 
}*/
