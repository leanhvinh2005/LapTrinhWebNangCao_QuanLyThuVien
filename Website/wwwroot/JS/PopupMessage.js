const popup = document.getElementById("popup");
popup.style.display = "block";

setTimeout(() => {
    popup.style.opacity = "0";
    setTimeout(() => popup.remove(), 500);
}, 3000);