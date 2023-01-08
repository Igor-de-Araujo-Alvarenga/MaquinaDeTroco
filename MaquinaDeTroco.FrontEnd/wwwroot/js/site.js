document.querySelector(".btn__sangria").addEventListener('click', () => {
    let modalSangria = document.querySelector(".modal__sangria");
    modalSangria.innerHTML = "<h4>Carregando...</h4>";

    fetch("https://localhost:7097/api/Register/Sangria")
        .then((response) => response.json())
        .then((data) => {
            let ul = document.createElement("ul");
            data.map((item) => {
               
                let li = document.createElement("li");

                li.innerText = "Moeda: " + item.moneyValue + " - Quantidade: " + item.quantity;
                ul.append(li)
            })
            modalSangria.innerHTML = "";
            modalSangria.append(ul);
        })
        .catch((err) => console.log(err))
})


document.querySelector(".btn__insert").addEventListener("click", () => {
    let inputQuantity = document.querySelectorAll(".input__quantity");
    let inputMoney = document.querySelectorAll(".input__money");

    let listMoney = []

    for (let i = 0; i < inputQuantity.length; i++) {
        let id = i + 1;
        let money = {
            id: id,
            moneyValue: inputMoney[i].value,
            quantity: inputQuantity[i].value
        }
        listMoney.push(money);
    }

    fetch("https://localhost:7097/api/Register/InserirMoedas",
        {
            method: 'PUT',
            headers: {
                'Access-Control-Allow-Method': '*',
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'Application/json',
                'accept': '*/*'
            },
            body: JSON.stringify(listMoney)
        }    )
        .then( async (response) => {
            if (response.status == 200)
                alert("Moedas inseridas com Sucesso!");
            else
                alert(await response.text());
        })
})



$(".input__inputMoney").maskMoney({
    prefix: 'R$ ',
    thousands: '.',
    decimal: ','
})
$(".input__price").maskMoney({
    prefix: 'R$ ',
    thousands: '.',
    decimal: ','
})
const format = {
    minimumFractionDigits: 2,
    style: 'currency',
    currency: 'BRL'
}

document.querySelector(".generate__change").addEventListener("click", () => {
    
    let inputMoney = document.querySelector(".input__inputMoney").value;
    let inputPrice = document.querySelector(".input__price").value;

    inputMoney = inputMoney.replace(",", ".").replace("R$", "").trim();
    inputPrice = inputPrice.replace(",", ".").replace("R$", "").trim();

    if (inputMoney == "" || inputPrice == "") {
        alert("Valores invalidos")
        return false;
    }

    let body = {
        input : inputMoney,
        price : inputPrice
    }

    fetch("https://localhost:7097/api/Register/GerarTroco",
        {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Method': '*',
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'Application/json',
                'accept': '*/*'
            },
            body: JSON.stringify(body)
        })
        .then((response) => response.json())
        .then((data) => {
            let ul = document.createElement("ul");
            if (typeof (data) == "string") {
                alert(JSON.stringify(data))
            } else {
                let totalChange = 0;
                data.map((item) => {
                    totalChange += item.moneyValue * item.quantity;
                    let li = document.createElement("li");
                    
                    li.innerText = "Moeda: " + item.moneyValue + " - Quantidade: " + item.quantity;
                    ul.append(li)
                })
                let total = document.createElement("p");
                total.textContent = "Troco total: " + totalChange.toFixed(2);;

                document.querySelector(".show__change").innerHTML = "";
                document.querySelector(".show__change").append(ul);
                document.querySelector(".show__change").append(total);
            }
        })
        .catch((err) => alert(err.message))

    
});



