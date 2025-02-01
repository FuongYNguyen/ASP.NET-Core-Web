function togglePopup() {
    var popup = document.getElementById("loginPopup");
    popup.style.display = (popup.style.display === "block") ? "none" : "block";
}

// Ẩn popup khi click ra ngoài
document.addEventListener("click", function (event) {
    var popup = document.getElementById("loginPopup");
    var userIcon = document.querySelector(".info-user");

    if (popup && !popup.contains(event.target) && !userIcon.contains(event.target)) {
        popup.style.display = "none";
    }
});
