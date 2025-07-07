function darkmode() {
    const element = document.body;
    const currentMode = element.classList.contains('dark-mode');
    const newMode = !currentMode;
    element.classList.toggle('dark-mode', newMode);
    localStorage.setItem('darkmode', newMode);
}

function onload() {
    document.body.classList.toggle('dark-mode', localStorage.getItem('darkmode') === 'true');
}