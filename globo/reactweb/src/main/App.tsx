import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HouseDetail from '../house/HouseDetail';
import HouseList from '../house/HouseList';
import './App.css';
import Header from './Header';

function App() {
	return (
		<BrowserRouter>
			<div className='container'>
				<Header subtitle='Providing houses all over Adiros world' />
				<Routes>
					<Route path='/' element={<HouseList />}></Route>
					<Route path='/houses/:id' element={<HouseDetail />}></Route>
				</Routes>
			</div>
		</BrowserRouter>
	);
}

export default App;
