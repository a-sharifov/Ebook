import { useState } from "react";
import "./Burger.css"

function Burger() {
    const [burger, setBurger] = useState("burger-bar unclicked")
    const [menu, setMenu] = useState("menu hidden")
    const [isMenuClicked, setIsMenuClicked] = useState(false)

    const updateMenu = () => {
        if (isMenuClicked) {
            setBurger("burger-bar unclicked")
            setMenu("menu hidden")
        }
        else {
            setBurger("burger-bar clicked")
            setMenu("menu visible")
        }
        setIsMenuClicked(!isMenuClicked)
    }

    return (
        <>
            <div className="burger-menu" onClick={updateMenu}>
                <div className={burger} />
                <div className={burger} />
                <div className={burger} />
            </div>
            <div className={menu}>
                <ul>
                    <li>Home</li>
                    <li>About</li>
                    <li>Genres</li>
                    <li>Authors</li>
                    <li>Lanquages</li>
                </ul>
            </div>
        </>
    )
}

export default Burger