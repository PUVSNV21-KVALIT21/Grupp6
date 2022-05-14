window.addEventListener("load", () => {
    localStorage.clear();
    document.querySelector(".bi-cart").style.color = null;
    document.querySelector("#amount").textContent = "";

    var options = { year: 'numeric', month: 'long', day: 'numeric' };
    var today = new Date();
    var todaystring = today.toLocaleDateString("sv-SE", options)

    document.querySelector(".date").textContent = todaystring;
    document.querySelector(".date2").textContent = todaystring;
});