import React from 'react';
import './Product.css';


function Product({name, author, price, productImage}) {
    return (
        <>
            <div className="product">
                <img src={productImage} alt="Product img" className="product__image" />
                <div className="home__product-info">
                    <h3 className="product__name">{name}</h3>
                    <p className="product__author">{author}</p>
                    <p className="product__price">{price}</p>
                </div>
            </div>
        </>
    )
}

export default Product