import {BrowserRouter} from 'react-router-dom';
import './App.css'
import PagesProvider from './PagesProvider';
import AppRouter from './AppRouter/AppRouter';

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
