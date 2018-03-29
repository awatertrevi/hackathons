package com.dalthow.healthpath.fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.dalthow.healthpath.R;
import com.dalthow.healthpath.model.Patient;

public class ProfileFragment extends Fragment
{
    private Patient patient;

    public ProfileFragment(Patient patient)
    {
        this.patient = patient;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        LinearLayout layout = (LinearLayout)inflater.inflate(R.layout.fragment_profile, container, false);

        TextView name = (TextView)layout.findViewById(R.id.patient_name);
        name.setText(patient.getName());

        TextView email = (TextView)layout.findViewById(R.id.patient_email);
        email.setText(patient.getEmail());

        TextView phone = (TextView)layout.findViewById(R.id.patient_phone);
        phone.setText(patient.getPhone());

        TextView birthday = (TextView)layout.findViewById(R.id.patient_birthday);
        birthday.setText(patient.getBirthday().toString());

        TextView insurance = (TextView)layout.findViewById(R.id.patient_insurance);
        insurance.setText(patient.getInsurance());

        TextView risk = (TextView)layout.findViewById(R.id.patient_risk);
        risk.setText(Float.toString(patient.getRisk()));

        TextView zip = (TextView)layout.findViewById(R.id.patient_zip);
        zip.setText(patient.getZip());

        TextView housenumber = (TextView)layout.findViewById(R.id.patient_number);
        housenumber.setText(Integer.toString(patient.getHouseNumber()));

        return layout;
    }
}

