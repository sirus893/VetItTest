import { FormGroupDirective } from "@angular/forms";

export class ProductModel {
    productId:number = 0;
    createDate:Date = new Date();
    updateDate?:Date;
    deleteDate?:Date;
    createdBy:string;
    updatedBy?:string;
    inactiveDate?:Date = null;
    organisationId:number;
    productCode:string;
    productDescription:string;
    supplierProductCode:string;
    supplierPrice:number;
    manufacturerCode:string;
    boughtInQuantity:number;
    soldInQuantity:number;
    soldInMarkup:number;
    wholesaleDiscount?:number;
    manufacturerDiscount?:number;
    useWholesaleDiscount:boolean;
    useManufacturerDiscount:boolean;
    dangerous?:boolean;
    neutering:boolean;
    euthanasia:boolean;
    bookWithoutServiceFee?:boolean;
    prescriptionOnly?:boolean;
}
