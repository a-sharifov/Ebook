import React from "react"
import "./Login.css"
import { Link } from "react-router-dom"

function Login() {
    return (
        <div className="login-container">
            <form className="login-form">
                <h2>Login</h2>
                <input type="text" placeholder="Email" />
                <input type="password" placeholder="Password" />
                <button>Login</button>
                

                <Link className="register-text" to="/register">Register</Link>
            </form>
        </div>
    )
}

export default Login