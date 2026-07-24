export function iniciarScrollSpy() {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
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
    }, {
        // En vez de exigir que se vea el 60% del ÁREA de la sección (imposible si la
        // sección es más alta que la pantalla), definimos una franja fina alrededor
        // del centro del viewport. La sección "activa" es la que está cruzando esa franja.
        threshold: 0,
        rootMargin: "-45% 0px -45% 0px"
    });

    document.querySelectorAll(".seccion").forEach(s => observer.observe(s));
}