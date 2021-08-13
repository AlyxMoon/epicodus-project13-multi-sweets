
function main () {
  const elPasswordToggles = document.querySelectorAll('.password-toggle')

  for (const elPasswordToggle of elPasswordToggles) {
    const elPasswordInput = elPasswordToggle.parentElement.querySelector('input')
    if (!elPasswordInput) return
  
    let showingPassword = false
  
    elPasswordToggle.addEventListener('click', function (event) {
      event.preventDefault();
  
      showingPassword = !showingPassword
      elPasswordInput.setAttribute('type', showingPassword ? 'text' : 'password')
  
      const eye1 = elPasswordToggle.querySelector('i:first-child')
      const eye2 = elPasswordToggle.querySelector('i:nth-child(2)')
  
      eye1.classList.toggle('bi-eye')
      eye1.classList.toggle('bi-eye-slash')
  
      eye2.classList.toggle('bi-eye-fill')
      eye2.classList.toggle('bi-eye-slash-fill')
    })
  }
}

main()
