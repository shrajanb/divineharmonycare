// Divine Harmony Care - Site JavaScript

// Navbar scroll effect
window.addEventListener('scroll', function () {
    const navbar = document.querySelector('.dhc-navbar');
    if (navbar) {
        if (window.scrollY > 50) {
            navbar.style.boxShadow = '0 4px 30px rgba(106, 63, 160, 0.15)';
        } else {
            navbar.style.boxShadow = '0 2px 20px rgba(106, 63, 160, 0.08)';
        }
    }
});

// Smooth scroll for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            e.preventDefault();
            target.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    });
});

// Auto-dismiss alerts after 5 seconds
document.querySelectorAll('.dhc-alert').forEach(alert => {
    setTimeout(() => {
        const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
        bsAlert.close();
    }, 5000);
});
