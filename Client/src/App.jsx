import './App.css'
import {UserIdContext} from "./context/Contexts.js";
import HomePage from "./pages/HomePage.jsx";

function App() {
  return (
    <>
        <UserIdContext.Provider value={1}>
            <HomePage/>
        </UserIdContext.Provider>
    </>
  )
}

export default App
