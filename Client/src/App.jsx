import {Route, Routes, BrowserRouter } from 'react-router-dom';
import './App.css'
import HomePage from "./pages/HomePage.jsx";
import Callback from './pages/Callback';
import PagesProvider from './PagesProvider';

function App() {
  return (  
    <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="callback" element={<Callback/>}/>
          </Routes>
    </BrowserRouter>  
  )
}

export default App
