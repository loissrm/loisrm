export function iniciarScrollSpy() {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Buscamos el link que coincide con el ID de la sección
                const id = entry.target.getAttribute("id");
                const navLinks = document.querySelectorAll(".menu a");
                navLinks.forEach(link => {
                    link.classList.remove("active");
                    if (link.getAttribute("href") === `#${id}`) {
                        link.classList.add("active");
                    }
                });
            }
        });
    }, { threshold: 0.6 });

    document.querySelectorAll(".seccion").forEach(s => observer.observe(s));
}