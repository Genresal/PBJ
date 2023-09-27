import {BrowserRouter} from 'react-router-dom';
import './App.css'
import PagesProvider from './modules/Provider/PagesProvider';
import AppRouter from './modules/AppRouter/AppRouter';

function App() {
  return (
    <PagesProvider>
      <BrowserRouter>
        <AppRouter/>
      </BrowserRouter>
    </PagesProvider>
  )
}

export default App
