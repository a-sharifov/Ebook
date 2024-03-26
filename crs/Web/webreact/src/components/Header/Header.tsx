import "./Header.css"
import { TbSearch } from "react-icons/tb"
import { AiOutlineHeart } from "react-icons/ai"
import { CgShoppingCart } from "react-icons/cg"
import { AiOutlineUser } from 'react-icons/ai';
import { useState } from "react";
import Burger from "./Burger/Burger";




function Header() {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    return (
        <header className="header">
            <div className="header-content">
                <div className="header-left">
                    <Burger/>
                    <h1>Ebook</h1>
                </div>
                <div className="header-right">
                    <TbSearch />
                    <AiOutlineHeart />
                    <CgShoppingCart />
                    {/* Показываем иконку пользователя, если пользователь вошел */}
                    {isLoggedIn ? <AiOutlineUser /> :
                        <button
                            className="login-button"
                            onClick={() => { setIsLoggedIn(true); }}>
                            Login
                        </button>}
                </div>
            </div>
        </header>
    );
}

export default Header;