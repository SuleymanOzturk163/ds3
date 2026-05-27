const cartElement = document.getElementById("sidebar-cart");
const cartItemsElement = document.getElementById("cart-items");
const cartTotalElement = document.getElementById("cart-total");
const closeCartButton = document.getElementById("close-cart");

// Yeni Buton Sınıf Seçicisi
const addToCartButtons = document.querySelectorAll(".add-to-cart-btn");

let cart = {};

// Sepeti Açma/Kapatma
const openCart = () => cartElement.classList.add("open");
const closeCart = () => cartElement.classList.remove("open");
closeCartButton.addEventListener("click", closeCart);

// Sepete Ürün Ekleme
addToCartButtons.forEach((button) => {
  button.addEventListener("click", (e) => {
    // Butona tıklandığında, butonun kendisi veya içindeki ikon hedef alınır.
    // Veriyi her zaman en üstteki butondan almalıyız.
    const targetButton = e.currentTarget;

    const id = targetButton.dataset.id;
    const name = targetButton.dataset.name;
    const price = parseFloat(targetButton.dataset.price);

    if (cart[id]) {
      cart[id].quantity += 1;
    } else {
      cart[id] = { id, name, price, quantity: 1 };
    }

    renderCart();
    openCart(); // Tıklanınca sepeti aç
  });
});

// Miktar Güncelleme (Bu fonksiyon HTML içinde çağrılacağı için global kalmalı)
window.updateQuantity = (id, change) => {
  if (cart[id]) {
    cart[id].quantity += change;

    if (cart[id].quantity <= 0) {
      delete cart[id];
    }
    renderCart();
  }
};

// Sepeti Çizme (Render Etme) İşlevi
const renderCart = () => {
  let itemsHtml = "";
  let total = 0;

  for (const id in cart) {
    const item = cart[id];
    const itemTotal = item.quantity * item.price;
    total += itemTotal;

    // TL sembolünü fiyatlara uygun ekledim
    itemsHtml += `
                <div class="cart-item">
                    <div class="item-details">
                        <div class="item-name">${item.name}</div>
                        <div class="item-price">${item.price.toFixed(
                          2
                        )} TL</div>
                    </div>
                    <div class="item-quantity">
                        <button class="quantity-btn" onclick="updateQuantity('${
                          item.id
                        }', -1)">-</button>
                        <span class="quantity-display">${item.quantity}</span>
                        <button class="quantity-btn" onclick="updateQuantity('${
                          item.id
                        }', 1)">+</button>
                    </div>
                </div>
            `;
  }

  if (Object.keys(cart).length === 0) {
    itemsHtml = `<p style="text-align: center; color: #999; padding: 20px 0;">Sepetiniz şu anda boş.</p>`;
  }

  cartItemsElement.innerHTML = itemsHtml;
  cartTotalElement.textContent = `${total.toFixed(2)} TL`;
};
