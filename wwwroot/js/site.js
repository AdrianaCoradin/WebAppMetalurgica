async function carregarProdutos() {
    try {
        const resposta = await fetch('https://localhost:7040/api/produtos');
        const produtos = await resposta.json();
        const container = document.getElementById('produtos');
        container.innerHTML = ''; 

        produtos.forEach(produto => {
            const box = document.createElement('div');
            box.classList.add('box');

            const imagem = document.createElement('img');
            imagem.src = produto.imagem;
            imagem.alt = produto.nome;

            const descricao = document.createElement('div');
            descricao.classList.add('description');
            descricao.textContent = produto.nome;

            const preco = document.createElement('div');
            preco.classList.add('price');
            preco.textContent = `R$ ${produto.preco.toFixed(2)}`;

            box.appendChild(imagem);
            box.appendChild(descricao);
            box.appendChild(preco);

            container.appendChild(box);

            imagem.onclick = function () {
                openModal(imagem.src, produto.nome);
            };
        });
    } catch (error) {
        console.error('Ocorreu um problema ao carregar, tente novamente mais tarde.', error);
    }
}

function openModal(src, caption) {
    const modal = document.getElementById('modal');
    const modalImg = document.getElementById('modalImage');
    const captionText = document.getElementById('caption');

    modal.style.display = "flex"; 
    modalImg.src = src; 
    //captionText.textContent = caption; 
}

document.getElementById('close').onclick = function () {
    document.getElementById('modal').style.display = "none";
};

document.addEventListener("DOMContentLoaded", carregarProdutos);
