import React from "react";
import Newsletter from "./Newsletter/Newsletter"
import './Home.css';
import Category from "./Category/Category";
import Products from "../Products/Products";

function Home() {
    return (<>
        <div className="home">
            <div className="home__banner">
                <img src="http://localhost:9001/api/v1/buckets/genres/objects/download?prefix=aG9ycm9yLnBuZw==
" alt="Banner" className="home__banner__image" />
            </div>
            <Category/>
            <Products title="All Products"/>
        </div>
        <Newsletter />
    </>)
}


export default Home