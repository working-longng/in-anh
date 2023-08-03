

$(window).on("load", function () {
    if ($('.data').data('islogin') == 'False') {
        setTimeout(function () {
            firebase.init()
        }, 500);
    } else {
        setTimeout(() => {
            firebase.initLogin();
        },500)
    }
    
})
var firebase = {
    init: async function () {
        await $('<link>')
            .appendTo('head')
            .attr({
                type: 'text/css',
                rel: 'stylesheet',
                href: 'https://www.gstatic.com/firebasejs/ui/6.0.1/firebase-ui-auth.css'
            });
        await $.getScript("https://www.gstatic.com/firebasejs/10.1.0/firebase-app-compat.js");
        await $.getScript("https://www.gstatic.com/firebasejs/ui/6.0.1/firebase-ui-auth.js");
        await $.getScript("https://www.gstatic.com/firebasejs/10.1.0/firebase-auth-compat.js");
        firebase.initializeApp({
            apiKey: 'AIzaSyAC14T-jU96RqlVo5nlkL92W9n9y3akniM',
            authDomain: 'jin-nie.firebaseapp.com',
            projectId: 'jin-nie',
            storageBucket: 'jin-nie.appspot.com',
            messagingSenderId: '543334601384',
            appId: '1:543334601384:web:271e306a00d4113c5ce89d',
            measurementId: 'G-414XY990V0',
        });
        var ui = new firebaseui.auth.AuthUI(firebase.auth());
        var uiConfig = {
            callbacks: {
                signInSuccessWithAuthResult: function (authResult, redirectUrl) {
                    firebase.auth().currentUser.getIdToken(/* forceRefresh */ true).then(function (idToken) {
                        $.ajax({
                            url: "/Auth",
                            data: { token: idToken }
                            , success: function (result) {                              
                                $('#firebaseui-auth-container').remove();
                                $('#user-login-nav').append(result);

                                setTimeout(() => {
                                    const toastcon = ` <div class="position-fixed top-0 end-0 p-3" style="z-index: 11">
            <div id="liveToast" class="toast hide" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true">
                <div class="toast-header">
                    <img lazy src="${authResult.additionalUserInfo.providerId == 'facebook.com' ? authResult.additionalUserInfo.profile.picture.data.url : authResult.additionalUserInfo.profile.picture}" class="img-photo-user rounded me-4" alt="...">
                    <strong class="me-auto">${authResult.additionalUserInfo.profile.name}</strong>
                    <small>just now</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">
                    Wellcome Back!
                </div>
            </div>
        </div>`
                                    $('.bootstrapinit').append(toastcon);
                                    test = $('#liveToast');
                                    const toast = bootstrap.Toast.getOrCreateInstance(test);
                                    toast.show();


                                }, 50)


                            }

                        });
                    }).catch(function (error) {
                        // Handle error
                    });




                    // User successfully signed in.
                    // Return type determines whether we continue the redirect automatically
                    // or whether we leave that to developer to handle.
                    return false;
                },
                uiShown: function () {
                    // The widget is rendered.
                    // Hide the loader.
                },
            },
            // Will use popup for IDP Providers sign-in flow instead of the default, redirect.
            signInFlow: 'popup',

            signInOptions: [
                // Leave the lines as is for the providers you want to offer your users.
                firebase.auth.GoogleAuthProvider.PROVIDER_ID,
                firebase.auth.FacebookAuthProvider.PROVIDER_ID,

            ],
        };
        ui.start('#firebaseui-auth-container', uiConfig);
    },
    initLogin: function () {
        
        
        
        setTimeout(() => {
            $.ajax({
                url: "/Auth/InitLogin",
                success: function (result) {
                    if (result.code != 200) {
                        
                    } else {
                        var data = JSON.parse(result.data);
                        const toastcon = ` <div class="position-fixed top-0 end-0 p-3" style="z-index: 11">
            <div id="liveToast" class="toast hide" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true">
                <div class="toast-header">
                    <img lazy src="${data.ImageUrlUser}" class="img-photo-user rounded me-4" alt="...">
                    <strong class="me-auto">${data.UserName}</strong>
                    <small>just now</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">
                    Wellcome Back!
                </div>
            </div>
        </div>`
                        $('.bootstrapinit').append(toastcon);
                        test = $('#liveToast');
                        const toast = bootstrap.Toast.getOrCreateInstance(test);
                        toast.show();
                    }

                }

            });
        }, 50)
    },
    logout: function () {
        $.removeCookie('userToken', { path: '/' });
        location.href = location.href;
    }

}
