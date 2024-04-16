 import React, { useEffect, useState } from "react";
import "./Register.css";
import { Link } from "react-router-dom";
import axios from "../../api/axios";

function Register() {

    const FIRST_NAME_REGEX = /^[a-zA-Z]+$/;
    const LAST_NAME_REGEX = /^[a-zA-Z]+$/;
    const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
    const EMAIL_REGEX = /^(.+)@(.+)$/;

    const [email, setEmail] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    const handleSubmit = async (e) => {
        try {
            // axios.get("/v1/users/register")
        }
        catch {

        }
    }

    useEffect(() => {
        fetchRoles();
        fetchGenders();
    }, []);

    const fetchRoles = () => {
        axios.get('/v1/users/roles')
            .then(response => {
                // setRoles(response.data)
                prompt(response.data)
            })
            .catch(error => {

                console.error('Error fetching roles:', error)
            })
    };

    const fetchGenders = () => {
        axios.get('/v1/users/genders')
            .then(response => {
                // setGenders(response.data)
                alert(response.data)
            })
            .catch(error => {
                console.error('Error fetching genders:', error)
            })
    }

    return (
        <div className="register-container">
            <form className="register-form">
                <h2>Register</h2>
                <div className="input-container">
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        placeholder="Email"
                        required
                        value={email}
                        onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className="input-container">
                    <label htmlFor="firstName">First name:</label>
                    <input
                        type="text"
                        id="firstName"
                        required
                        placeholder="First name"
                        value={firstName}
                        onChange={(e) => setFirstName(e.target.value)} />
                </div>
                <div className="input-container">
                    <label htmlFor="lastName">Last name:</label>
                    <input
                        type="text"
                        id="lastName"
                        required
                        placeholder="Last name"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)} />
                </div>
                <div className="input-container">
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        required
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div className="input-container">
                    <label htmlFor="confirmPassword">Confirm password:</label>
                    <input
                        type="password"
                        id="confirmPassword"
                        placeholder="Confirm password"
                        required
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)} />
                </div>
                <button type="submit">Register</button>
                <Link className="login-text" to="/login">login</Link>
            </form>
        </div>
    )
}

export default Register
