document.addEventListener('DOMContentLoaded', () => {

    let $btnLimpar = document.querySelector('#btnLimpar')
    let $selectMes = document.querySelector('#selectMes')
    let $selectNome = document.querySelector('#selectNome')

    $btnLimpar.addEventListener('click', () => {
        $selectMes.value = ''
        $selectNome.value = ''
    })

})