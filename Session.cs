// using UserApp;
//
// namespace SessionApp;
//
// // enum UserType
// // {
// //     User,
// //     Guest
// // }
//
// class Session
// {
//     DateTime CreatedAt { get; set; }
//     DateTime UpdatedAt { get; set; }
//     // UserType UserType;
//
//     public Session(Guid userId)
//     {
//         CreatedAt = DateTime.Now;
//         UpdatedAt = DateTime.Now;
//         // UserType = user_type;
//     }
//
//     // TODO: simulate Access-Refresh token later
//     public void UpdateSession()
//     {
//         UpdatedAt = DateTime.Now;
//     }
//
//     public void TerminateSession()
//     {
//         CreatedAt = DateTime.Now;
//         UpdatedAt = DateTime.Now;
//     }
//
//     // public bool isAuthorized()
//     // {
//     //     return UserId != Guid.Empty;
//     // }
//
//     bool HasUserExists(string userIdentity, List<User> users)
//     {
//         return users.Any(user => user.Username == userIdentity || user.Email == userIdentity);
//     }
//
//     public bool TryLogIn(string userIdentity, string password, List<User> users)
//     {
//         if (HasUserExists(userIdentity, users))
//         {
//             User user = users.Find(user => user.Username == userIdentity || user.Email == userIdentity);
//             return user.Password == password;
//         }
//
//         return false;
//     }
// }