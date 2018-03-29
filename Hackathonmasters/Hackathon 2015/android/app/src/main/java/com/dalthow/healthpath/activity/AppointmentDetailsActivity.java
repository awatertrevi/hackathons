package com.dalthow.healthpath.activity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.design.widget.AppBarLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import com.dalthow.healthpath.R;
import com.dalthow.healthpath.model.Appointment;


public class AppointmentDetailsActivity extends AppCompatActivity {

    private Gson gson;

    String appointmentID;
    RecyclerView mRecycle1, mRecycle2;
    TextView mTextView;
    Appointment mainAppointment;


    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_appointment_detail);

        gson = new GsonBuilder()
                .enableComplexMapKeySerialization()
                .serializeNulls()
                .create();

        Intent intent = getIntent();

        Log.d("TEST", intent.getStringExtra("test"));

        Appointment obj = gson.fromJson(intent.getStringExtra("test"), Appointment.class);

        CardView quCard = (CardView)findViewById(R.id.qa);
        quCard.setVisibility(obj.getAppointmentResult() != null ? View.VISIBLE : View.INVISIBLE);

        CardView resultCard = (CardView)findViewById(R.id.results);
        resultCard.setVisibility(obj.getAppointmentResult() != null ? View.VISIBLE : View.INVISIBLE);

        Log.d("REAL-TEST", obj.getSubject());

        String quTextString = "";

        if(obj.getAppointmentResult() != null)
        {
            if(obj.getAppointmentResult().getQuestions() != null)
            {
                for(int i = 0; i < obj.getAppointmentResult().getQuestions().size(); i++)
                {
                    quTextString = "<b>" + obj.getAppointmentResult().getQuestions().get(i).getQuestion() + "</b> " + obj.getAppointmentResult().getQuestions().get(i).getAnswer() + "<br/><br/>";
                }
            }

            TextView prescription = (TextView)findViewById(R.id.appointment_detail_prescription);
            prescription.setText(obj.getAppointmentResult().getPrescription());

            TextView diagnose = (TextView)findViewById(R.id.appointment_detail_diagnose);
            diagnose.setText(obj.getAppointmentResult().getDiagnose());

            TextView quText = (TextView)findViewById(R.id.quText);
            quText.setText(Html.fromHtml(quTextString));

        }

        TextView subjectTextView = (TextView)findViewById(R.id.appointment_detail_subject);
        subjectTextView.setText(obj.getSubject());

        TextView dateTextView = (TextView)findViewById(R.id.appointment_detail_date);
        dateTextView.setText(obj.getAppointmentDate().toString());

        TextView doctorTextView = (TextView)findViewById(R.id.appointment_detail_doctor);
        doctorTextView.setText(Integer.toString(obj.getDoctorId()));

        TextView symptomsTextView = (TextView)findViewById(R.id.appointment_detail_symptoms);
        symptomsTextView.setText(obj.getSymptoms());
    }
}
