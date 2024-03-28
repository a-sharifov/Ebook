import React from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import './App.css'
import Header from './components/Header/Header'
import Footer from './components/Footer/Footer'
import Home from './components/Home/Home'
import Category from './components/Category/Category'

function App() {
    return (
        <BrowserRouter>
            <Header />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/category/:id" element={<Category/>} />
                {/*<Route path="/cart" element={<Cart/>} />*/}
                {/*<Route path="/wish" element={<Wish/>} />*/}
                {/*<Route path="/product" element={<Product/>} />*/}
                {/*<Route path="/author" element={<Author/>} />*/}
            </Routes>
            <Footer />
        </BrowserRouter>
    )
}

export default App
