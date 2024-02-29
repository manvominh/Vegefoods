//#region css files
import './App.css';

import './assets/css/open-iconic-bootstrap.min.css';
import './assets/css/animate.css';

//import './assets/css/owl.carousel.min.css';
//import './assets/css/owl.theme.default.min.css';
//import './assets/css/magnific-popup.css';

import './assets/css/aos.css';

import './assets/css/ionicons.min.css';

//import './assets/css/bootstrap-datepicker.css';
//import './assets/css/jquery.timepicker.css';

import './assets/css/flaticon.css';
import './assets/css/icomoon.css';
import './assets/css/style.css';
//#endregion

import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import Home from './components/Home';
import Header from './components/Header';
import Footer from './components/Footer';
import About from './components/About';
import Login from './components/Login';
import Register from './components/Register';
import Profile from './components/Profile';
import Shop from './components/Shop';
import Cart from './components/Cart';
import { ShopContextProvider } from "./context/ShopContext";

function App() {
  return (
    <div className="App">
      <ToastContainer theme='colored' position='top-center'></ToastContainer>
      <ShopContextProvider>
      <BrowserRouter>
      <Header ></Header>
      <Routes>
        <Route path='/' element={<Home/>}></Route>
        <Route path='/login' element={<Login/>}></Route>
        <Route path='/register' element={<Register/>}></Route>   
        <Route path='/profile' element={<Profile/>}></Route> 
        <Route path='/shop' element={<Shop/>}></Route>
        <Route path='/cart' element={<Cart/>}></Route>
        <Route path='/about' element={<About/>}></Route>
      </Routes>      
      </BrowserRouter>
      <Footer></Footer>
      </ShopContextProvider>
    </div>
  );
}

export default App;
