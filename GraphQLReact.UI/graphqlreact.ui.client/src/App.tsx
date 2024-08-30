import React, { useState } from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import ProductsList from "./components/ProductList";
import CategoryList from "./components/CategoryList";

const App: React.FC = () => {
  const [activeTab, setActiveTab] = useState<"products" | "categories">(
    "products"
  );

  return (
    <div className="App">
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="#home">My App</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link
              href="#products"
              onClick={() => setActiveTab("products")}
              active={activeTab === "products"}
            >
              Products
            </Nav.Link>
            <Nav.Link
              href="#categories"
              onClick={() => setActiveTab("categories")}
              active={activeTab === "categories"}
            >
              Categories
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Container className="mt-4">
        {activeTab === "products" && <ProductsList />}
        {activeTab === "categories" && <CategoryList />}
      </Container>
    </div>
  );
};

export default App;
