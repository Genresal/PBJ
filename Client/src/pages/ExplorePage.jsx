import { FormControl, Grid, Input, InputAdornment, TextField } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search';
import React from 'react'
import { useState } from 'react';
import NavMenu from '../UI/NavMenu/NavMenu';
import { getUsersByEmailPartAsync } from '../modules/User/api/getUsersByEmailPartAsync';
import UserCard from '../modules/User/components/UserCard/UserCard';

export default function ExplorePage() {
    const [emailPart, setEmailPart] = useState("");
    const [users, setUsers] = useState([]);

    const handleSearchChange = (event) => {
        console.log(event.target.value)

        setUsers([])

        getUsersByEmailPartAsync(event.target.value, 5)
        .then(response => setUsers(response.data));
    }

    return (
        <>
            <NavMenu/>

            <Grid container style={{width: 500, marginTop: 10}}>
                <Grid item style={{width: 500}}>
                    <FormControl fullWidth>
                        <TextField
                        multiline
                        fullWidth
                        placeholder="Search..."
                        onChange={handleSearchChange}
                        InputProps={{
                            startAdornment: (
                                <InputAdornment position="start">
                                    <SearchIcon/>
                                </InputAdornment>
                            )
                        }}/>
                    </FormControl>
                </Grid>
                <Grid item style={{width: 500, marginTop: 20}}>
                    {users.map(user => 
                            <UserCard key={user.email} user={user} handleFollowersClick={null} isButtonEnabled={false}/>
                        )
                    }
                </Grid>
            </Grid>
        </>
    )
}
