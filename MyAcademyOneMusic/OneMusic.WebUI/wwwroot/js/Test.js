var SessisonExpTimeSec = 60;

function StartTimer() {
    SessisonExpTimeSec--;
    if (SessisonExpTimeSec < 0) {
        window.clearInterval(deger);
        Swal.fire({
            title: "Oturum süresi Doldu!",
            text: "Uzun süredir işlem yapmadığınız için aktif otrumunuzun süresi doldu lütfen tekrar giriş yapın.!",
            icon: "info"
        });
        
    }
}
var deger = window.setInterval('StartTimer()', 1000);