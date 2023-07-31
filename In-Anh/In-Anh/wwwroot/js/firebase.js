

$(window).on("load", function () {
    setTimeout(function () {
        firebase.init()
    }, 2000); })
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
        await  $.getScript("https://www.gstatic.com/firebasejs/10.1.0/firebase-auth-compat.js");
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
                    console.log(authResult);
                    localStorage.setItem('userLogin', JSON.stringify(authResult));
                    setLoader(false);
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
                {
                    provider: firebase.auth.PhoneAuthProvider.PROVIDER_ID,
                    recaptchaParameters: {
                        type: 'image', // 'audio'
                        size: 'normal', // 'invisible' or 'compact'
                        badge: 'bottomleft', //' bottomright' or 'inline' applies to invisible.
                    },
                    defaultCountry: 'VN', // Set default country to the United Kingdom (+44).
                },
            ],
        };
        ui.start('#firebaseui-auth-container', uiConfig);
    },

}
