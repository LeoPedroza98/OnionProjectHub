import logo from './logo.svg';
import './App.css';
import NavBar from './Components/NavBar';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import HomePage from './Pages/HomePage';
import Dashboard from './Pages/Dashboard';

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <HomePage />
  },
  {
    path: "/dashboard",
    element: <Dashboard/>
  }
  ])
  return (
    
    <RouterProvider router={router}/>
  );
}

export default App;
