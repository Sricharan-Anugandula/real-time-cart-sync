import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class CartService {
  private hubConnection!: signalR.HubConnection;
  cart: any[] = [];

  constructor(private http: HttpClient) {}

  connect(userId: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7162/cartHub') // backend URL
      .build();

    this.hubConnection.start().then(() => {
      console.log('Connected to SignalR');
      this.hubConnection.invoke('JoinCart', userId);
    });

    this.hubConnection.on('CartUpdated', (updatedCart) => {
      console.log('Cart updated:', updatedCart);
      this.cart = updatedCart;
    });
  }

  addItem(item: any) {
    return this.http.post('https://localhost:7162/api/Cart/add', item);
  }

  getCart(userId: string) {
    return this.http.get(`https://localhost:7162/api/Cart//${userId}`);
  }
}
