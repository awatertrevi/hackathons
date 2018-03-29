package com.dalthow.healthpath.handler;

import android.util.Log;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.StringRequest;

import java.util.HashMap;
import java.util.Map;

import com.dalthow.healthpath.ApplicationController;
import com.dalthow.healthpath.R;

public class NetworkHandler {
    private static volatile NetworkHandler instance = null;

    public NetworkHandler() {
    }

    public static synchronized NetworkHandler getInstance() {
        if (instance == null) {
            instance = new NetworkHandler();
        }
        return instance;
    }

    public void Login() {
        String tag_login = "login";

        StringRequest sr = new StringRequest(Request.Method.POST, ApplicationController.getInstance().getString(R.string.login_page) , new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                Log.d("POSTMAN", response.toString());
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error)
            {
                VolleyLog.d("POSTMAN", "Error: " + error.getMessage());
                Log.d("POSTMAN", ""+error.getMessage()+","+error.toString());
            }
        }){
            @Override
            protected Map<String,String> getParams(){
                Map<String, String> params = new HashMap<String, String>();
                params.put("email", "luke@paradoxis.nl");
                params.put("password", "password");

                return params;
            }
        };

            ApplicationController.getInstance().addToRequestQueue(sr, tag_login);
    }
}
