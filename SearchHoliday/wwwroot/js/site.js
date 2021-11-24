document.addEventListener('DOMContentLoaded', () => {
    if (!localStorage.getItem('disclamer')) {
        document.querySelector('.disclamer-btn').addEventListener('click', () => {
            document.querySelector('.disclamer').remove()
            localStorage.setItem('disclamer', true)
        })
    } else {
        document.querySelector('.disclamer').remove()
    }

    if (document.querySelector('.page-control') != null) {
        const pageControl = document.querySelector('.page-control')
        const pageControlBtnsPrev = pageControl.querySelector('.page-control__btns-prev')
        const pageControlBtnsNext = pageControl.querySelector('.page-control__btns-next')
        const href = window.location.href
        let request = "1"

        if (href.indexOf('?') != -1) request = href.substring(href.indexOf('?') + 5);

        const pageControlUl = document.createElement('ul');
        pageControlUl.className = 'page-control__num nav justify-content-center'
        if (request <= 2) {
            pageControlUl.innerHTML = 
                `
                    <li class="nav-item">
                        <a href="?num=1" class="nav-link">1</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=2" class="nav-link">2</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=3" class="nav-link">3</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=4" class="nav-link">4</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=261" class="nav-link">261</a>
                    </li> 
                `
        } else if ((request > 2) && (request <= 4)) {
            pageControlUl.innerHTML = 
                `
                    <li class="nav-item">
                        <a href="?num=1" class="nav-link">1</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=2" class="nav-link">2</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=3" class="nav-link">3</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=4" class="nav-link">4</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=5" class="nav-link">5</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=261" class="nav-link">261</a>
                    </li> 
                `
        } else if ((request >= 5) && (request < 258)) {
            pageControlUl.innerHTML = 
                `
                    <li class="nav-item">
                        <a href="?num=1" class="nav-link">1</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=${+request - 2}" class="nav-link">${+request - 2}</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=${+request - 1}" class="nav-link">${+request - 1}</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=${+request}" class="nav-link">${+request}</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=${+request + 1}" class="nav-link">${+request + 1}</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=${+request + 2}" class="nav-link">${+request + 2}</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=261" class="nav-link">261</a>
                    </li>
                `
        } else if (request >= 260) {
            pageControlUl.innerHTML =
                `
                    <li class="nav-item">
                        <a href="?num=1" class="nav-link">1</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=258" class="nav-link">258</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=260" class="nav-link">259</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=260" class="nav-link">260</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=261" class="nav-link">261</a>
                    </li> 
                `
        } else if ((request >= 258) && (request < 260)) {
            pageControlUl.innerHTML =
                `
                    <li class="nav-item">
                        <a href="?num=1" class="nav-link">1</a>
                    </li>
                    <li class="nav-item divider">
                        <a class="nav-link divider">...</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=257" class="nav-link">257</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=258" class="nav-link">258</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=259" class="nav-link">259</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=260" class="nav-link">260</a>
                    </li>
                    <li class="nav-item">
                        <a href="?num=261" class="nav-link">261</a>
                    </li> 
                `
        }

        pageControl.prepend(pageControlUl);

        const allItem = pageControlUl.querySelectorAll('.nav-item');
        allItem.forEach(item => {
            item.classList.remove('active')
            if (item.textContent.trim() == request) item.classList.add('active')
        })

        if (request == 1) pageControlBtnsPrev.disabled = true
        else pageControlBtnsPrev.disable = false

        if (request == 261) pageControlBtnsNext.disabled = true
        else pageControlBtnsNext.disabled = false

        pageControlBtnsPrev.addEventListener('click', () => {
            if (request > 1) request = +request - 1
            if (href.indexOf('?') != -1) location.href = href.substring(0, href.indexOf('?')) + `?num=${request}`
            else location.href = href + `?num=${request}`
        })

        pageControlBtnsNext.addEventListener('click', () => {
            if (request < 261) request = +request + 1
            if (href.indexOf('?') != -1) location.href = href.substring(0, href.indexOf('?')) + `?num=${request}`
            else location.href = href + `?num=${request}`
        })
    }
})