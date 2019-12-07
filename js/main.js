var db = firebase.firestore()

function addProject() {
    db.collection("projects").add({
        image: 'https://images.unsplash.com/photo-1558981403-c5f9899a28bc?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80',
        description: 'test description',
        name: 'test'
    })
}

async function getAllProjects() {
    return (await db.collection("projects").get()).docs.map(doc => {
        let data = doc.data();
        return { name: data.name, image: data.image, description: data.description }
    })
}

async function logIn() {
    let provider = new firebase.auth.GithubAuthProvider();
    return ((user) => ({
        email: user['email'],
        name: user['displayName']
    }))((await firebase.auth().signInWithPopup(provider)).user);
}

async function logOut() {
    await firebase.auth().signOut()
}