import React from "react";
import onionImage from "../Assets/onion.png"
import "../Styles/NavBarStyle.css"

const NavBar = (props) => (
    <>
      <div className="d-flex flex-column flex-shrink-0 p-3" style={{ width: '280px', height: '100vh',backgroundColor: 'black' }}>
        <ul className="nav nav-pills flex-column mb-auto">
          <li className="nav-item">
            <a href="/" className={props.Tela == "home" ? "nav-link active" : "nav-link"} aria-current="page">
            <i class="fa-solid fa-house"></i>
              Home
            </a>
          </li>
          <li className="nav-item">
            <a href="/dashboard" className={props.Tela == "dashboard" ? "nav-link active" : "nav-link"} aria-current="page" >
              Dashboard
            </a>
          </li>
        </ul>
        <hr />
      </div>
    </>
  );
  
export default NavBar;