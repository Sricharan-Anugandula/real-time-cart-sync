import { Component, OnInit, inject } from '@angular/core';
import { CartService } from './cart.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [],
  template: `
    <h2>🛒 Real-Time Cart</h2>
    <button (click)="addToCart()">Add Random Item</button>
 <ul>
  @for (item of cartService.cart; track item.productId) {
    <li>
      {{ item.productId }} — Qty: {{ item.quantity }}
    </li>
  }
</ul> `
})
export class AppComponent implements OnInit {
  userId = 'user123';
  cartService = inject(CartService);

  ngOnInit(): void {
    this.cartService.connect(this.userId);
    this.cartService.getCart(this.userId).subscribe((data: any) => {
      this.cartService.cart = data;
    });
  }

  addToCart() {
    const item = {
      userId: this.userId,
      productId: 'product-' + Math.floor(Math.random() * 100),
      quantity: 1
    };

    this.cartService.addItem(item).subscribe();
  }
}
