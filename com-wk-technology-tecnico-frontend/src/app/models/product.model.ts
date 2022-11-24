import { BaseModel } from "./base.model";
import { Category } from "./category.model";

export class Product extends BaseModel{

    /// <summary> 
    /// pt-BR: Nome 
    /// </summary> 
    name : string;

    /// <summary> 
    /// pt-BR: Descrição 
    /// </summary> 
    description : string;

    /// <summary> 
    /// pt-BR: Preço unitário 
    /// </summary> 
    unitPrice : number;

    /// <summary> 
    /// pt-BR: Quantidade 
    /// </summary> 
    amount : number;

    /// <summary> 
    /// pt-BR: Categoria 
    /// </summary> 
    category : Category;
}