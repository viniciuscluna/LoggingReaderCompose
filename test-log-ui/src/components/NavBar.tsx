import React, { useState } from 'react';
import {
  Navbar,
  NavbarBrand,
} from 'reactstrap';

function NavBar() {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar>
        <NavbarBrand>LogSearch - Sample</NavbarBrand>
        
      </Navbar>
    </div>
  );
}

export default NavBar;