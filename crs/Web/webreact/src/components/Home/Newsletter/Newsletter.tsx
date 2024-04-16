import './Newsletter.css';

const Newsletter = () => {
  return (
    <div className="newsletter">
      <h2 className="newsletter__title">Subscribe to our newsletter</h2>
      <form className="newsletter__form">
        <input
          type="email"
          className="newsletter__input"
          placeholder="Enter your email"
        />
        <button type="submit" className="newsletter__button">
        Subscribe
        </button>
      </form>
    </div>
  );
};

export default Newsletter;
