const userMenu = document.getElementById('userMenu');
const dropdown = document.getElementById('dropdown');

userMenu.addEventListener('click', (e) => {
    e.stopPropagation(); // Impede que o clique se aplica para fora do documento
    dropdown.classList.toggle('active');
});

document.addEventListener('click', () => {
    dropdown.classList.remove('active');
})