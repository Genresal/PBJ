import { useState } from 'react'
import { Box, Modal, TextField, Button, Grid } from '@mui/material';

export default function EditUserModal({ loggedUser, onOpen, onClose, onSave }) {
    const [editedUser, setEditedUser] = useState({...loggedUser})

    const handleInputChange = (event) => {
        const { name, value } = event.target;

        setEditedUser({
          ...editedUser,
          [name]: value
        });
    };
    
    const handleSave = () => {
        onSave(editedUser);
        
        onClose();
    };

    return (
        <Modal open={onOpen} onClose={onClose}>
            <Box sx={{ 
                    width: 350, 
                    p: 4, 
                    bgcolor: 'background.paper',
                    borderRadius: '4px',
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    border: '2px solid #000',
                    transform: 'translate(-50%, -50%)',
                }
                }>
                <Grid container direction="column" rowSpacing={2} md={12} style={{margin: "0 auto"}}>
                    <h2 style={{textAlign: "center"}}>Edit User</h2>
                    <Grid item>
                        <TextField
                        label="Name"
                        name="userName"
                        fullWidth
                        value={editedUser.userName}
                        onChange={handleInputChange}
                        />
                    </Grid>
                    <Grid item>
                        <TextField
                        label="Surname"
                        name="surname"
                        fullWidth
                        value={editedUser.surname}
                        onChange={handleInputChange}
                        />
                    </Grid>
                    <Grid item>
                        <TextField
                        label="Birth Date"
                        name="birthDate"
                        type="date"
                        fullWidth
                        value={editedUser.birthDate}
                        onChange={handleInputChange}
                        />
                    </Grid>
                    <Grid item alignSelf="center">
                        <Button variant="contained" onClick={handleSave}>
                            Save Changes
                        </Button>
                    </Grid>
                </Grid>
            </Box>
        </Modal>
    )
}
