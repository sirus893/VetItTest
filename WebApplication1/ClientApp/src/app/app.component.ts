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


  // call api for active products
  // If no products return, inform the user and make sure the data is reverted to all products.
  // TODO: Need a better way of doing this.
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

  // Same as above only for drugs.
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

  // duplicated code
  // TODO can I move this to services?
  getAllProducts() {
    this.service.getAllProducts().subscribe(data => {
      this.service.changeProductList(data);
    });
  }


}
