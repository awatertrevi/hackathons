package com.dalthow.healthpath.fragments;

import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.dalthow.healthpath.R;
import com.dalthow.healthpath.activity.MainActivity;
import com.dalthow.healthpath.adapters.SimpleAppointmentRecyclerViewAdapter;
import com.dalthow.healthpath.model.Appointment;

public class AppointmentFragment extends Fragment {

    List<Appointment> appointmentList = MainActivity.testPatient.getAppointments();
    FloatingActionButton toggleHistory;
    RelativeLayout rv;
    private boolean sortState = true;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        rv = (RelativeLayout) inflater.inflate(R.layout.fragment_appointments_list, container, false);

        toggleHistory = (FloatingActionButton)rv.findViewById(R.id.toggle_history);
        toggleHistory.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                filterList();
            }
        });

        filterList();

        return rv;
    }

    private void filterList()
    {
        sortState ^= true;

        List<Appointment> newList = new ArrayList<Appointment>();

        for (Appointment appointment : MainActivity.testPatient.getAppointments())
        {
           if(sortState)
           {
               if(appointment.getAppointmentDate().after(new Date()))
               {
                   newList.add(appointment);
                   Toast.makeText(getActivity().getApplicationContext(), "Toekomstige Afspraken", Toast.LENGTH_SHORT).show();
               }
           }

           else
           {
               if(appointment.getAppointmentDate().before(new Date()))
               {
                   newList.add(appointment);
                   Toast.makeText(getActivity().getApplicationContext(), "Afspraken Geschiedenis", Toast.LENGTH_SHORT).show();
               }
           }
        }

        appointmentList = newList;

        setupRecyclerView(rv);
    }

    private void setupRecyclerView(RelativeLayout layout) {
        RecyclerView rc = (RecyclerView)layout.findViewById(R.id.recyclerview);

        rc.setLayoutManager(new LinearLayoutManager(rc.getContext()));
        rc.setAdapter(new SimpleAppointmentRecyclerViewAdapter(getActivity(), appointmentList, this));
        rc.setItemAnimator(new DefaultItemAnimator());
        rc.setHasFixedSize(true);

    }
}

