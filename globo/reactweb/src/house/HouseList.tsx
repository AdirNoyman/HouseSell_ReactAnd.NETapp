// import { useState } from 'react';
// import { idText } from 'typescript';
import { currencyFormatter } from '../config';
import useFetchHouses from '../hooks/HouseHooks';
// import { House } from '../types/house';
import ApiStatus from '../ApiStatus';
import { Link, useNavigate } from 'react-router-dom';

const HouseList = () => {
	const navigateTo = useNavigate();
	const { data, status, isSuccess } = useFetchHouses();

	if (!isSuccess) return <ApiStatus status={status} />;

	return (
		<div>
			<div className='row mb-2'>
				<h5 className='themeFontColor text-center'>
					Houses currently on the market
				</h5>
			</div>
			<table className='table table-hover'>
				<thead>
					<tr>
						<th>Address</th>
						<th>Country</th>
						<th>Asking Price</th>
					</tr>
				</thead>
				<tbody>
					{data &&
						data.map(house => (
							<tr
								key={house.id}
								onClick={() =>
									navigateTo(`/houses/${house.id}`)
								}>
								<td>{house.address}</td>
								<td>{house.country}</td>
								<td>{currencyFormatter.format(house.price)}</td>
							</tr>
						))}
				</tbody>
			</table>
			{/* <tbody>
					{data &&
						data.map((h: House) => (
							<tr
								key={h.id}
								onClick={() => nav(`/house/${h.id}`)}>
								<td>{h.address}</td>
								<td>{h.country}</td>
								<td>{currencyFormatter.format(h.price)}</td>
							</tr>
						))}
				</tbody> */}

			<Link className='btn btn-primary' to='/houses/add'>
				Add
			</Link>
		</div>
	);
};

export default HouseList;
