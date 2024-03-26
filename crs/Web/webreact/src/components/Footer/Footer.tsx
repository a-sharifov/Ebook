import './Footer.css';


const Footer = () => {
    // Функция для копирования текста в буфер обмена
    const copyToClipboard = (text) => {
        navigator.clipboard.writeText(text);

    };

    const phoneNumber = "999-999-9-99";
    const address = "Baku, Street X, Apartment Y";
    const email = "user@example.com";

    return (
        <footer className="footer">
            <div className="footer__section" onClick={() => copyToClipboard(phoneNumber)}>
                <h3 className="footer__section-title">Phone Number</h3>
                <p className="footer__section-content">999-999-9-99</p>
            </div>
            <div className="footer__section" onClick={() => copyToClipboard(address)}>
                <h3 className="footer__section-title">Adress</h3>
                <p className="footer__section-content">Baku, Street X, Apartment Y</p>
            </div>
            <div className="footer__section" onClick={() => copyToClipboard(email)}>
                <h3 className="footer__section-title">Email</h3>
                <p className="footer__section-content">user.shar@gmail.com</p>
            </div>
        </footer>
    );
};

export default Footer;
