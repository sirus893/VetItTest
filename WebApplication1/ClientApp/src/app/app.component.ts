import { Component } from '@angular/core';
import { ProductModel } from '../models/product.model';
import { ProductService } from '../Services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(public service: ProductService) { }

  public productList: ProductModel;


  activeProducts() {
    this.service.getActiveProducts().subscribe(data => {
      if (data === undefined || data === null) {
        alert("No products found. Loading all products");
        this.getAllProducts();
      }
      else {
        this.service.changeProductList(data);
      }

    });
  }

  dangerousDrugs() {
    this.service.getDangerousDrugs().subscribe(data => {
      if (data === undefined || data === null) {
        alert("No products found. Loading all products");
        this.getAllProducts();
      }
      else {
        this.service.changeProductList(data);
      }
    })
  }

  getAllProducts() {
    this.service.getAllProducts().subscribe(data => {
      this.service.changeProductList(data);
    });
  }


}
