import React from 'react';
import './Footer.css';


const Footer = () => {
    return (
        <div className="footer">
            <div className="footer-content">
                <div className="col">
                    <div className="title">About</div>
                    <div className="text">
                        Discover a rich selection of physical books at our online bookstore.
                        Browse through captivating novels,
                        insightful guides, and educational materials,
                        available for delivery to your doorstep.
                    </div>
                </div>
                <div className="col">
                    <div className="title">Contact</div>
                    <div className="c-item">
                        <div className="text">
                            Azerbaijan, Baku, AZ1007, Narimanov rayon
                        </div>
                    </div>
                    <div className="c-item">
                        <div className="text">Phone: +994-50-970-83-27</div>
                    </div>
                    <div className="c-item">
                        <div className="text">Email: akber.sharifov2004@gmail.com</div>
                    </div>
                </div>
                <div className="col">
                    <div className="title">Genres</div>
                    <span className="text">Horror</span>
                    <span className="text">Roman</span>
                    <span className="text">Comedy</span>
                    <span className="text">Manga</span>
                    <span className="text">Fantasy</span>
                    <span className="text">Fairy tale</span>
                </div>
                <div className="col">
                    <div className="title">Pages</div>
                    <span className="text">Home</span>
                    <span className="text">About</span>
                    <span className="text">Privacy Policy</span>
                    <span className="text">Returns</span>
                    <span className="text">Terms & Conditions</span>
                    <span className="text">Contact Us</span>
                </div>
            </div>
            <div className="bottom-bar">
                <div className="bottom-bar-content">
                    <span className="text">
                        @Akber Sharifov 2024
                    </span>
                </div>
            </div>
        </div>
    )
};

export default Footer;
