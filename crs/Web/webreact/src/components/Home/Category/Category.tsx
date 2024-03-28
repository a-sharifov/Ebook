import { useNavigate, useParams } from "react-router-dom";
import "./Category.css";
import React from "react";

const Category = () => {
  const { id } = useParams();
  return (
    <div className="shop-by-category">
      <div className="categories">
        {/* {categories?.data?.map((item) => ( */}
        <div
          // key={item.id}
          className="category"
        // onClick={() => navigate(`/category/${item.id}`)}
        >
          <img
            src="https://th.bing.com/th/id/R.fc7b6a1cf7f6acdebe9726f8731b06cc?rik=WFFIUNl5rrj%2fJg&pid=ImgRaw&r=0"
          />
        </div>
        <div
          // key={item.id}
          className="category"
        // onClick={() => navigate(`/category/${item.id}`)}
        >
          <img
            src="https://th.bing.com/th/id/R.fc7b6a1cf7f6acdebe9726f8731b06cc?rik=WFFIUNl5rrj%2fJg&pid=ImgRaw&r=0"
          />
        </div>
        <div
          // key={item.id}
          className="category"
        // onClick={() => navigate(`/category/${item.id}`)}
        >
          <img
            src="https://th.bing.com/th/id/R.fc7b6a1cf7f6acdebe9726f8731b06cc?rik=WFFIUNl5rrj%2fJg&pid=ImgRaw&r=0"
          />
        </div>
        <div
          // key={item.id}
          className="category"
        // onClick={() => navigate(`/category/${item.id}`)}
        >
          <img
            src="https://th.bing.com/th/id/R.fc7b6a1cf7f6acdebe9726f8731b06cc?rik=WFFIUNl5rrj%2fJg&pid=ImgRaw&r=0"
          />
        </div>
        {/* ))} */}
      </div>
    </div>
  );
};

export default Category;