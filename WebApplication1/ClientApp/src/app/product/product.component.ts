import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { ProductService } from '../../Services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [
  ]
})
export class ProductComponent implements OnInit {

  public productList: ProductModel;

  ModalTitle: string;
  prod: any;
  ActivateEditDescription: boolean = false;

  constructor(public service:ProductService) { }

  ngOnInit(): void {
    this.getAllProducts();
    this.service.currentProductList.subscribe(productList => this.productList = productList);
  }


  getAllProducts() {
    this.service.getAllProducts().subscribe(data => {
      this.service.changeProductList(data);
      
    });
  }


  closeClick() {
    this.ActivateEditDescription = false;
    this.getAllProducts();
  }

  editClick(product) {
    this.prod = product;
    this.ModalTitle = "Edit Product Description";
    this.ActivateEditDescription = true;
  }
}
